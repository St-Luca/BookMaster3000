<script lang="ts" setup>
import type { Author } from '~/entities/author';
import type { Book } from '~/entities/book';

const props = defineProps<{
  book?: Book,
}>();

const emit = defineEmits([ 'clickAuthor', 'clickSubject' ]);

const badges = computed(() => [
  {
    name: props.book?.authors.length == 1 ? "Автор" : "Авторы",
    items: props.book?.authors.map(author => author.name) ?? [],
    clickHandler: (author:Author) => emit('clickAuthor', author)
  },
  {
    name: props.book?.subjects?.length == 1 ? "Тема" : "Темы",
    items: props.book?.subjects ?? [],
    clickHandler: (subject:string) => emit('clickSubject', subject)
  },

])
</script>

<template>
  <UCard
    class="relative overflow-y-scroll pt-0"
  >
    <div v-if="book" class="flex gap-4">
      <div class="grow">
        <h3 class="text-2xl">
          {{ book.title }}
        </h3>
        <p
          v-if="book.subtitle"
          class="text-sm text-gray-700"
        >
          {{ book.subtitle }}
        </p>
        <div class="flex flex-col gap-2 mt-2 mb-3">
          <div
            v-for="badge,i in badges.filter(b => b.items?.length)"
            :key="i"
            class="flex items-baseline gap-2"
          >
            <span class="text-sm">{{ badge.name }}: </span>
            <div>
              <UBadge
                v-for="item in badge.items"
                color="gray"
                class="mr-1 cursor-pointer"
                @click="badge.clickHandler(item as any)"
              >
                {{ item }}
              </UBadge>
            </div>
          </div>
        </div>
        <p v-if="book.firstPublishDate">
          Опубликована: {{ book.firstPublishDate }} г.
        </p>
        <div class="mt-5">
          <h4 class="text-lg m-0">Описание:</h4>
          <p>{{ book.description }}</p>
        </div>
      </div>

      <div class="w-[200px]">
        <div class="bg-gray-200 w-full aspect-[3/4]">
          <img
            v-if="book.coverImg"
            :src="book.coverImg"
            alt=""
            class="object-cover w-full h-full"
          >
        </div>
      </div>
    </div>
    <div
      v-else
      class="flex justify-center items-center h-full"
    >
      Выберите книгу для просмотра
    </div>
  </UCard>
</template>