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
}>();

const emit = defineEmits([ 'select' ]);

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
    class="relative overflow-y-scroll"
    :ui="{
      body: {
        padding: 'pt-0 md:pt-0 pb-3 md:pb-3 pr-3 md:pr-3',
      },
    }"
  >
    <UTable
      :columns="(cols as any)"
      :rows="rows"
      :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: emptyText }"
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
      @select="(row:RowsType) => emit('select', row.ref)"
    />
  </UCard>
</template> 