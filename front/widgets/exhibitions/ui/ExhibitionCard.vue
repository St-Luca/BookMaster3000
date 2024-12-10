<script lang="ts" setup>
import type { Exhibition } from '~/entities/exhibition';
import BookCard from '~/widgets/books/ui/BookCard.vue';

const props = defineProps<{
  data: Exhibition;
}>();

const booksShown = ref(false);
</script>

<template>
  <div class="py-4">
    <h3 class="text-lg mb-1">{{ data.name }}</h3>
    <p>Дата создания: {{ data.dateCreated }}</p>
    <p class="text-gray-800">{{ data.description }}</p>
    <div>
      <div
        class="inline-block text-primary-500 cursor-pointer mt-1 select-none"
        @click="booksShown = !booksShown"
      >
        {{ !booksShown ? 'Показать список книг' : 'Скрыть список книг'}}
      </div>
    </div>
    <div v-if="booksShown" class="grid grid-cols-2 gap-4 my-4">
      <BookCard v-for="book in data.books" :book="book"></BookCard>
    </div>
  </div>
</template>
