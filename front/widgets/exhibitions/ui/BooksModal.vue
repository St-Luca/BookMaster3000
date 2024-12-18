<script setup lang="ts">
import type { Book } from '~/entities/book';
import { searchBook } from '~/entities/book/api/search';
import type { Exhibition } from '~/entities/exhibition';
import { getExhibitionById } from '~/entities/exhibition/api/get';
import { useExhibitionsStore } from '~/entities/exhibition/store';
import debounce from 'debounce'

const exhibitionsStore = useExhibitionsStore();

const model = defineModel<boolean>();

const props = defineProps<{
  exhibitionId: string|number,
}>();

watch(model, (val) => {
  if (val) exhibitionRefresh();
});

const { data: exhibition, refresh: exhibitionRefresh, status } = useAsyncData(async () => {
  const ex = await getExhibitionById(props.exhibitionId);
  if (ex) booksListRefresh();
  return ex;
}, { immediate: false });

const { data: booksList, refresh: booksListRefresh, status: booksListStatus } = useAsyncData(async () => {
  const res = await searchBook({
    title: bookTitle.value,
    author: "",
    subject: "",
    page: page.value,
  }) ?? [];

  const list = res ? res.books : [];
  if (res) itemsCount.value = res.itemsCount;
  const exBooks = exhibition.value?.books ?? [];

  return [
    ...(page.value == 1 && bookTitle.value.trim() === '' ? exBooks : []),
    ...list.filter(
      book => !exBooks.find(exBook => book.id == exBook.id)
    ),
  ]
}, { immediate: false });

const bookTitle = ref("");
const page = ref(1);
const itemsCount = ref(1);

watch(page, () => booksListRefresh());

const bookTitleDebounce = debounce(booksListRefresh, 200);

const hasBook = (book:Book) => {
  return exhibitionsStore.hasBook(props.exhibitionId, book.id);
}

const pendingArr = ref<Array<string|number>>([]);

const toggleBook = async (book:Book) => {
  if (!exhibition.value) return;
  pendingArr.value.push(book.id);
  exhibitionsStore.toggleBook(book, exhibition.value);
  pendingArr.value = pendingArr.value.filter(id => id !== book.id);
}
</script>

<template>
  <UModal v-model="model">
    <UCard
      class="max-h-[75vh] h-[75vh] overflow-hidden"
      :ui="{
        body: {
          base: 'h-full overflow-hidden',
        },
      }"
    >
      <div
        v-if="exhibition"
        class="h-full flex flex-col justify-between overflow-hidden min-h-0"
      >
        <h2 class="text-xl mb-3">{{exhibition.name}}</h2>
        <UInput
          v-model="bookTitle"
          placeholder="Название книги"
          :ui="{
            color: {
              white: {
                outline: 'focus:ring-1 focus:ring-inset focus:ring-gray-300'
              }
            }
          }"
          :loading="booksListStatus == 'pending' && bookTitle !== ''"
          trailing
          class="mb-2"
          @update:modelValue="bookTitleDebounce"
        />
        <div class="pr-1.5 grow min-h-0 overflow-y-auto scrollbar-w-[3px] scrollbar scrollbar-thumb-slate-400 scrollbar-thumb-rounded-[4px]">
          <div class="flex flex-col divide-y divide-gray-200">
            <div
              v-for="book in booksList"
              class="flex items-center justify-between py-2 first:pt-1"
            >
              <p>{{ book.title }}</p>
              <UButton
                class="w-[100px] justify-center duration-150"
                :class="{ 'hover:bg-red-50 bg-transparent': hasBook(book) }"
                :color="!hasBook(book) ? 'primary': 'red'"
                variant="soft"
                :loading="pendingArr.includes(book.id)"
                :disabled="pendingArr.includes(book.id)"
                @click="toggleBook(book)"
              >
                {{ !hasBook(book) ? 'Добавить' : 'Убрать' }}
              </UButton>
            </div>
          </div>
        </div>
        <div class="flex justify-end pt-2">
          <UPagination
            v-model="page"
            :total="itemsCount" 
            :pageCount="50"
            :size="'xs'"
          />
        </div>
        <UButton class="justify-center py-2 mt-4" @click="model = false">Готово</UButton>
      </div>
      <div v-else>Выставка не найдена</div>
    </UCard>
  </UModal>
</template>