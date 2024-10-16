<script lang="ts" setup>
import { searchBook } from '~/entities/book/api/search';
import { type Book, type BookSearchParams } from '~/entities/book/types';
import SearchParams from '~/features/browse/ui/SearchParams.vue';

const emit = defineEmits<{
  (event: 'result', value: Book[]): void
}>();

const searchParams = ref<BookSearchParams>({
  title: "",
  author: "",
  subject: "",
});

const fieldNames = ref<{[key in keyof BookSearchParams]: string}>({
  title: "Название",
  author: "Автор",
  subject: "Тема",
});

const handleSubmit = () => {
  searchBook(searchParams.value)
    .then(res => emit('result', res))
    .catch(err => emit('result', []));
};
</script>

<template>
  <SearchParams
    :modelValue="searchParams"
    :fieldNames="fieldNames"
    @submit="handleSubmit"
  ></SearchParams>
</template>