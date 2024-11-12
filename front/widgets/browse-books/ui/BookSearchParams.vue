<script lang="ts" setup>
import { searchBook } from '~/entities/book/api/search';
import { type Book, type BookListResponse, type BookSearchParams } from '~/entities/book/types';
import SearchParams from '~/features/browse/ui/SearchParams.vue';

const props = defineProps<{
  page?: number;
}>();

const emit = defineEmits<{
  (event: 'result', value: BookListResponse|false): void
}>();

const searchParams = ref<Partial<BookSearchParams>>({
  title: "",
  author: "",
  subject: "",
});

const fieldNames = ref<{[key in keyof BookSearchParams]?: string}>({
  title: "Название",
  author: "Автор",
  subject: "Тема",
});

const handleSubmit = () => {
  searchBook({
    ...searchParams.value,
    page: props.page ?? 1,
  })
    .then(res => emit('result', res))
    .catch(err => emit('result', false));
};

watch(() => props.page, handleSubmit);
</script>

<template>
  <SearchParams
    :modelValue="searchParams"
    :fieldNames="fieldNames"
    @submit="handleSubmit"
  ></SearchParams>
</template>