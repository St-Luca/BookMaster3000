<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import CirculationTable from './CirculationTable.vue';
import { issueBook } from '~/entities/circulation-record/api/issue';
import { returnBook } from '~/entities/circulation-record/api/return';
import { extendBook } from '~/entities/circulation-record/api/extend';

const props = defineProps<{
  customer: Customer;
}>();

const emit = defineEmits<{
  (event: 'refresh', value: void): void,
}>();

const issueBookId = ref('');

const handleIssue = () => {
  issueBook(props.customer.id, issueBookId.value).then(res => {
    if (res) emit("refresh")
  });
}

const handleExtend = (bookId: string|number) => {
  extendBook(props.customer.id, bookId)
    .then(res => {
      if (res) emit("refresh");
    })
}

const handleReturn = (bookId: string|number) => {
  returnBook(props.customer.id, bookId)
    .then(res => {
      if (res) emit("refresh");
    })
}
</script>

<template>
  <UCard class="min-h-[0]" :ui="{ body: { base: 'max-h-full h-full min-h-[0] flex flex-col justify-between gap-8' } }">
    <div class="">
      <div class="flex items-baseline gap-6 mb-8">
        <h2 class="text-xl">Выдача и возврат книг</h2>
        <p class="text-gray-600">{{ customer.name }}</p>
      </div>
      <form
        class="flex gap-4"
        @submit.prevent="handleIssue"
      >
        <div class="flex items-baseline gap-2 grow">
          <p>ID книги:</p>
          <UInput
            v-model="issueBookId"
            class="grow"
          />
        </div>
        <div class="flex gap-2">
          <UButton type="submit" class="px-6">Выдать</UButton>
        </div>
      </form>
    </div>
    <div class="flex flex-col justify-between grow min-h-[0]">
      <div class="flex flex-col" :style="{ height: 'calc(50% - 16px)', maxHeight: 'calc(50% - 16px)' }">
        <h3 class="text-lg mb-1">Выданные книги</h3>
        <CirculationTable
          :items="customer.issuedBooks ?? []"
          class="h-full"
          @extend="handleExtend"
          @return="handleReturn"
        />
      </div>
      <div class="flex flex-col" :style="{ height: 'calc(50% - 16px)', maxHeight: 'calc(50% - 16px)' }">
        <h3 class="text-lg mb-1">История</h3>
        <CirculationTable
          :items="customer.returnedBooks ?? []"
          class="h-full"
          past
        />
      </div>
    </div>
  </UCard>
</template>