<script lang="ts" setup>
import type { Author } from '~/entities/author';
import type { Book } from '~/entities/book';
import { useAuthStore } from '~/features/auth/store';
import { DetailInfo } from '~/features/browse';
import { parseDate } from '~/shared/util';
import ExhibitionsModal from './ExhibitionsModal.vue';

const authStore = useAuthStore();

const props = defineProps<{
  book?: Book,
}>();

const emit = defineEmits([ 'clickAuthor', 'clickSubject', 'exhibitionsModalToggle' ]);

const badges = computed(() => [
  {
    name: props.book?.authors.length == 1 ? "Автор" : "Авторы",
    items: props.book?.authors.map(author => ({
      ref: author,
      label: author.name,
    })) ?? [],
    clickHandler: (author:Author) => {
      emit('clickAuthor', author);
      mode.value = 'author';
      viewedAuthor.value = author;
    }
  },
  {
    name: props.book?.subjects?.length == 1 ? "Тема" : "Темы",
    items: props.book?.subjects?.map(subject => ({
      ref: subject,
      label: subject,
    })) ?? [],
    clickHandler: (subject:string) => emit('clickSubject', subject)
  },
]);

const mode = ref<'book'|'author'>('book');
const viewedAuthor = ref<Author|undefined>();

const authorYears = computed<string|undefined>(() => {
  if (!viewedAuthor.value) return undefined;

  const birth = viewedAuthor.value.birthDate ? parseDate(viewedAuthor.value.birthDate) : undefined;
  const death = viewedAuthor.value.deathDate ? parseDate(viewedAuthor.value.deathDate) : undefined;

  if (birth) {
    if (death)
      return `${birth} — ${death} гг.`
    return `род. в ${birth} году`
  }
  if (death) return `умер нерождённым в ${death} году`;
  return undefined;
});

const exhibitionsModalOpen = ref(false);

const handleExhibitionsClick = () => {
  console.log(exhibitionsModalOpen.value);
  exhibitionsModalOpen.value = !exhibitionsModalOpen.value;
}

watch(exhibitionsModalOpen, (val) => emit("exhibitionsModalToggle", val))
</script>

<template>
  <UCard
    class="relative overflow-y-auto pt-0"
  >
    <div v-if="book">
      <DetailInfo
        v-if="mode == 'book' || !viewedAuthor"
        :title="book.title"
        :subtitle="book.subtitle"
        :img="book.coverImg"
        :noImagePolicy="'blank'"
        :badges="badges"
        class="flex gap-4"
      >
        <template #default>
          <div>
            <p v-if="book.firstPublishDate">
              Опубликована: {{ book.firstPublishDate }} г.
            </p>
            <div class="mt-5">
              <h4 class="text-lg m-0">Описание:</h4>
              <p>{{ book.description }}</p>
            </div>
            <UButton
              v-if="authStore.isAuth"
              class="mt-2"
              @click="handleExhibitionsClick"
            >
              Настроить выставки
            </UButton>
          </div>
        </template>
      </DetailInfo>
      <DetailInfo
        v-if="mode == 'author' && viewedAuthor"
        :title="viewedAuthor.name"
        :subtitle="authorYears"
        :noImagePolicy="'blank'"
        class="flex gap-4"
      >
        <template #title-tailing>
          <div
            class="text-sm text-gray-500 hover:underline hover:text-gray-600 cursor-pointer"
            @click="mode='book'"
          >
            Вернуться к книге
          </div>
        </template>
        <template #default>
          <div>
            <p v-if="viewedAuthor.wikipedia">
              <a :href="viewedAuthor.wikipedia">Страница в Википедии</a>
            </p>
            <div v-if="viewedAuthor.bio" class="mt-5">
              <h4 class="text-lg m-0">Биография:</h4>
              <p>{{ viewedAuthor.bio }}</p>
            </div>
          </div>
        </template>
      </DetailInfo>
    </div>
    <div
      v-else
      class="absolute top-0 bottom-0 left-0 right-0 flex justify-center items-center h-full"
    >
      Выберите книгу для просмотра
    </div>

    <KeepAlive>
      <ExhibitionsModal v-model="exhibitionsModalOpen" :book="book!" />
    </KeepAlive>
  </UCard>
</template>