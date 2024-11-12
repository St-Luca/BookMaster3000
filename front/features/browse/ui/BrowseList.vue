<script lang="ts" setup generic="ListedType extends { id: any }, RowsType extends {ref: ListedType}">

interface Col {
  key: string;
  label: string;
}

const props = defineProps<{
  list: ListedType[]|undefined,
  highlightedItem?: ListedType,
  rows: (item:ListedType) => RowsType,
  cols: Col[],
  page: number,
  totalItems: number,
}>();

const localPage = computed({
  get() {
    return props.page;
  },
  set(value) {
    emit('update:page', value);
  }
})

const emit = defineEmits([ 'select', 'update:page' ]);

const rows = computed(() => props.list?.map(item => ({
  ...props.rows(item),
  class: props.highlightedItem && item.id === props.highlightedItem.id ? 'bg-blue-50 hover:!bg-blue-50' : '',
})) ?? [])

const emptyText = computed(() => props.list === undefined
  ? 'Введите запрос для поиска'
  : 'Ничего не найдено'
)
</script>

<template>
  <UCard
    :ui="{
      body: {
        base: 'relative h-full flex flex-col',
        padding: 'p-0 md:p-0',
      },
    }"
  >
    <div class="overflow-y-scroll grow pl-2 md:pl-2">
      <UTable
        :columns="(cols as any)"
        :rows="rows"
        :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: emptyText }"
        class=""
        :ui="{
          wrapper: ({ base: '' } as any),
          base: 'divide-y-0',
          th: {
            base: `
              sticky top-0 bg-white !border-b-0
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
        @select="(row:RowsType) => emit('select', row.ref)"
      />
    </div>
    <div
      v-if="list && list.length > 0"
      class="flex justify-end border-t border-gray-200 pt-2 mb-2 pr-2 bg-white"
    >
      <UPagination
        v-model="localPage"
        :total="totalItems"
        :pageCount="50"
        :size="'xs'"
      ></UPagination>
    </div>
  </UCard>
</template> 