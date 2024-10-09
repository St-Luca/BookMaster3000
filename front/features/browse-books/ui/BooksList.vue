<script lang="ts" setup>
import type { Book } from '~/entities/book';

const props = defineProps<{
  list: Book[],
  viewedBook?: Book,
}>();

const emit = defineEmits([ 'bookSelect' ]);

interface Row {
  ref: Book;
  name: string;
  author: string;
  class: string;
}

interface Col {
  key: keyof Row;
  label: string;
}

const cols = ref<Col[]>([
  {
    label: 'Название',
    key: 'name'
  },
  {
    label: 'Авторы',
    key: 'author'
  }
])

const rows = computed<Row[]>(() => props.list.map(book => ({
  ref: book,
  name: book.title,
  author: book.authors.map(author => author.name).join(', '),
  class: props.viewedBook && book.key === props.viewedBook?.key ? 'bg-blue-50 hover:bg-blue-50' : '',
})))
</script>

<template>
  <UCard
    class="relative overflow-y-scroll pt-0"
    :ui="{
      body: {
        padding: 'pt-0 md:pt-0 pb-3 md:pb-3 pr-3 md:pr-3',
      },
    }"
  >
    <UTable
      :columns="cols"
      :rows="rows"
      :ui="{
        wrapper: ({ base: '' } as any),
        base: 'divide-y-0',
        th: { 
          base: `
            sticky top-0 pt-5 bg-white !border-b-0
            after:content-[\'\'] after:bg-gray-200 after:h-[1px] after:w-full after:absolute after:bottom-0 after:left-0
          `,
          padding: 'py-3',
        },
        td: {
          base: 'text-gray-800 cursor-pointer',
          padding: 'py-2',
        },
        tr: {
          base: 'hover:bg-gray-100 duration-100',
        },
      }"
      @select="(row:Row) => emit('bookSelect', row.ref)"
    />
  </UCard>
</template> 