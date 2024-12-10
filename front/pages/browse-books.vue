<script lang="ts" setup>
import type { Book } from '~/entities/book';
import type { BookListResponse } from '~/entities/book/types';
import { BrowseLayout, BrowseList } from '~/features/browse';
import { BookCard, BookSearchParams } from '~/widgets/books';

definePageMeta({
  layout: 'full-height',
  middleware: 'auth',
})

const booksList = ref<Book[]|undefined>(undefined);

const viewedBook = ref<Book|undefined>(undefined);

const handleBookSelect = (book: Book) => {
  viewedBook.value = book;
}

const cols = ref([
  {
    label: 'Название',
    key: 'name'
  },
  {
    label: 'Авторы',
    key: 'author'
  }
])

const rows = (book:Book) => ({
  ref: book,
  name: book.title,
  author: book.authors.map(author => author.name).join(', '),
})

const page = ref(1);
const totalItems = ref(0);

const handleSearchResult = (res: BookListResponse|false) => {
  if (!res) {
    booksList.value = [];
    return;
  }
  booksList.value = res.books;
  // if (!booksList.value.find(b => b.id == viewedBook.value?.id)) {
  //   viewedBook.value = undefined;
  // }
  totalItems.value = res.itemsCount;
}
</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <BookSearchParams
        class="h-full"
        :page="page"
        @result="handleSearchResult"
      />
    </template>
    <template #top>
      <BrowseList
        :list="booksList"
        :rows="rows"
        :cols="cols"
        :highlightedItem="viewedBook"
        v-model:page="page"
        :totalItems="totalItems"
        class="h-full"
        @select="handleBookSelect"
      />
    </template>
    <template v-if="viewedBook" #bottom>
      <BookCard
        :book="viewedBook"
        :key="viewedBook?.id"
        class="h-full"
      />
    </template>
  </BrowseLayout>
</template>