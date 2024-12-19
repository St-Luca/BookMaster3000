<script lang="ts" setup>
import { getRemindersReport } from '~/entities/customer/api/reminders';
import { parseDate } from '~/shared/util';

const { data } = useAsyncData(async () => {
  const response = await getRemindersReport();
  return response ?? [];
})

const cols = ref([
  {
    key: 'bookId',
    label: 'ID',
  },
  {
    key: 'bookTitle',
    label: 'Название книги',
  },
  {
    key: 'clientName',
    label: 'Имя клиента',
  },
  {
    key: 'issueFrom',
    label: 'Выдана',
  },
  {
    key: 'issueTo',
    label: 'Вернуть до',
  },
])

const rows = computed(() => data.value?.map(item => ({
  ...item,
  issueFrom: item.issueFrom.toISOString().substring(0, 10),
  issueTo: item.issueTo.toISOString().substring(0, 10),
})));
</script>

<template>
  <div>
    <UTable
      :columns="(cols as any)"
      :rows="rows"
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
          base: 'text-gray-800',
          padding: 'py-2',
        },
      }"
    ></UTable>
  </div>
</template>