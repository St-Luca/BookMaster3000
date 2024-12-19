<script lang="ts" setup>
import type { Exhibition } from '~/entities/exhibition';
import { useAuthStore } from '~/features/auth/store';
import BookCard from '~/widgets/books/ui/BookCard.vue';
import BooksModal from './BooksModal.vue';

const authStore = useAuthStore();

const props = defineProps<{
  data: Exhibition;
}>();

const emit = defineEmits(['bookCardExhibitionsModalToggle'])

const bookListShown = ref(false);
const booksModalShown = ref(false);
</script>

<template>
  <div class="py-4">
    <h3 class="text-lg mb-1">{{ data.name }}</h3>
    <!-- <p>Дата создания: {{ data.dateCreated }}</p> -->
    <p class="text-gray-800">{{ data.description }}</p>
    <div class="flex gap-6">
      <div
        v-if="authStore.isAuth"
        class="inline-block text-primary-500 cursor-pointer mt-1 select-none"
        @click="booksModalShown = !booksModalShown"
      >
        Редактировать список книг
      </div>
      <div
        class="inline-block text-primary-500 cursor-pointer mt-1 select-none"
        @click="bookListShown = !bookListShown"
      >
        {{ !bookListShown ? 'Показать список книг' : 'Скрыть список книг'}}
      </div>
    </div>
    <div v-if="bookListShown" class="grid grid-cols-2 gap-4 my-4">
      <BookCard
        v-for="book in data.books"
        :book="book"
        @exhibitionsModalToggle="val => emit('bookCardExhibitionsModalToggle', val)"
      />
    </div>
    <BooksModal :exhibitionId="data.id" v-model="booksModalShown" :key="data.id"></BooksModal>
  </div>
</template>
