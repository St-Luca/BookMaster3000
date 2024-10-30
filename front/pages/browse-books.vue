<script lang="ts" setup>
import type { Book } from '~/entities/book';
import { BrowseLayout, BrowseList } from '~/features/browse';
import { BookCard, BookSearchParams } from '~/widgets/browse-books';

definePageMeta({
  layout: 'full-height'
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

const handleSearchResult = (res: Book[]) => {
  booksList.value = res;
  if (!booksList.value.find(b => b.id == viewedBook.value?.id)) {
    viewedBook.value = undefined;
  }
}
</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <BookSearchParams class="h-full" @result="handleSearchResult"></BookSearchParams>
    </template>
    <template #top>
      <BrowseList
        :list="booksList"
        :rows="rows"
        :cols="cols"
        :highlightedItem="viewedBook"
        @select="handleBookSelect"
        class="h-full"
      />
    </template>
    <template #bottom>
      <BookCard
        :book="viewedBook"
        :key="viewedBook?.id"
        class="h-full"
      />
    </template>
  </BrowseLayout>
</template>