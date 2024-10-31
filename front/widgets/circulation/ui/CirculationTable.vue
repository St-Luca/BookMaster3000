<script lang="ts" setup>
import type { CirculationRecord } from '~/entities/circulation-record';
import type { Customer } from '~/entities/customer';

const props = defineProps<{
  items: CirculationRecord[];
  past?: boolean;
}>();

const emit = defineEmits<{
  (event: 'return', value: string|number): void,
  (event: 'extend', value: string|number): void,
}>();

const cols = ref([
  {
    key: 'name',
    label: 'Название',
  },
  {
    key: 'issued',
    label: 'Выдана',
  },
  ...[
    props.past
      ? {
        key: 'returned',
        label: 'Возвращено',
      }
      : {
        key: 'returnPlanned',
        label: 'Вернуть до',
      },
  ],
  {
    key: 'actions',
    class: 'w-5'
  }
])

interface Row {
  bookId: string|number;
  name: string;
  issued: string;
  returned: string;
  returnPlanned: string;
}

const rows = computed<Row[]>(() => props.items.map(item => ({
  bookId: item.book.id,
  name: item.book.title,
  issued: item.issueDate.toISOString().substring(0, 10),
  returnPlanned: item.returnDatePlanned.toISOString().substring(0, 10),
  returned: item.returnDateActual?.toISOString().substring(0, 10) ?? "Не возвращено",
})))

const actionItems = (row: Row) => [
  [
    {
      label: 'Вернуть',
      click: () => emit('return', row.bookId),
    },
    {
      label: 'Продлить',
      click: () => emit('extend', row.bookId),
    }
  ]
]
</script>

<template>
  <!-- <div class="relative "> -->
    <UTable
      :columns="cols"
      :rows="rows"
      :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'Нет записей' }"
      :ui="{
        wrapper: ({ base: 'h-full overflow-y-auto' } as any),
        base: 'divide-y-0',
        th: {
          base: `
            sticky top-0 bg-white !border-b-0 z-[1]
            after:content-[\'\'] after:bg-gray-200 after:h-[1px] after:w-full after:absolute after:bottom-0 after:left-0
          `,
          padding: 'py-3',
        },
        td: {
          base: 'text-gray-800 cursor-default',
          padding: 'py-2',
        },
      }"
    >
      <template v-if="!past" #actions-data="{ row }">
        <UDropdown :items="actionItems(row)">
          <UButton color="gray" variant="ghost" icon="i-heroicons-ellipsis-horizontal-20-solid" />
        </UDropdown>
      </template>
    </UTable>
  <!-- </div> -->
</template>