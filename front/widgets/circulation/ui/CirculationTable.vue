<script lang="ts" setup>
import type { CirculationRecord } from '~/entities/circulation-record';
import type { Customer } from '~/entities/customer';

const props = defineProps<{
  items: CirculationRecord[];
  past?: boolean;
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
  ]
])

const rows = computed(() => props.items.map(item => ({
  name: item.book.title,
  issued: item.issueDate.toISOString().substring(0, 10),
  returnPlanned: item.returnDatePlanned.toISOString().substring(0, 10),
  returned: item.returnDateActual?.toISOString().substring(0, 10) ?? "Не возвращено",
})))
</script>

<template>
  <!-- <div class="relative "> -->
    <UTable
      :columns="cols"
      :rows="rows"
      :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'Нет записей' }"
      class="h-full"
      :ui="{
        wrapper: ({ base: 'h-full overflow-y-auto' } as any),
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
    />
  <!-- </div> -->
</template>