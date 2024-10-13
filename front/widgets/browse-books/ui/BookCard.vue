<script lang="ts" setup>
import type { Author } from '~/entities/author';
import type { Book } from '~/entities/book';
import { DetailInfo } from '~/features/browse';
import { parseDate } from '~/shared/util';

const props = defineProps<{
  book?: Book,
}>();

const emit = defineEmits([ 'clickAuthor', 'clickSubject' ]);

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
})
</script>

<template>
  <UCard
    class="relative overflow-y-scroll pt-0"
  >
    <div v-if="book">
      <DetailInfo
        v-if="mode == 'book' || !viewedAuthor"
        :title="book.title"
        :subtitle="book.subtitle"
        :image="book.coverImg"
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
      class="flex justify-center items-center h-full"
    >
      Выберите книгу для просмотра
    </div>
  </UCard>
</template>