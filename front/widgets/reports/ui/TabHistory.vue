<script lang="ts" setup>
import { getBookCirculationHistory } from '~/entities/book/api/history';
import { getRemindersReport } from '~/entities/customer/api/reminders';

const { data, refresh } = useAsyncData(async () => {
  const response = await getBookCirculationHistory(bookId.value);
  return response ?? [];
}, { immediate: false })

const bookId = ref('');

const cols = ref([
  {
    key: 'clientName',
    label: 'Имя клиента',
  },
  {
    key: 'issueFrom',
    label: 'Выдана',
  },
  {
    key: 'returnDate',
    label: 'Дата возвращения',
  },
])

const rows = computed(() => data.value?.map(item => ({
  ...item,
  issueFrom: item.issueFrom.toISOString().substring(0, 10),
  returnDate: item.issueTo.toISOString().substring(0, 10),
})));
</script>

<template>
  <div class="flex gap-8">
    <div class="p-4 w-1/6">
      <form class="" @submit.prevent="() => refresh()">
        <UFormGroup
          label="ID книги"
        >
          <UInput v-model="bookId"></UInput>
        </UFormGroup>
        <UButton type="submit" class="mt-4 w-full justify-center">Получить историю</UButton>
      </form>
      <div v-if="data?.length" class="mt-4 text-lg">
        <h2>{{ data[0].bookTitle }}</h2>
      </div>
    </div>
    <div class="grow">
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
  </div>
</template>