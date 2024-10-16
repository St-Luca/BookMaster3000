<script lang="ts" setup>
import type { Author } from '~/entities/author';
import type { Customer } from '~/entities/customer';
import { DetailInfo } from '~/features/browse';
import { parseDate } from '~/shared/util';
import { CustomerModal } from '..';

const props = defineProps<{
  customer?: Customer,
}>();

const params = ref({
  address: "Адрес", 
  zip: "Индекс", 
  city: "Город", 
  phone: "Телефон", 
  email: "E-mail",
})

const isOpenModal = ref(false);
</script>

<template>
  <UCard
    class="relative overflow-y-scroll pt-0"
  >
    <div v-if="customer" class="flex divide-x divide-gray-200">
      <div class="pr-6 w-[40%]">
        <h3 class="flex items-baseline text-xl mb-3">
          <div>{{ customer.name }}</div>
        </h3>
        <div>
          <div class="flex items-baseline gap-4 mb-1">
            <h4 class="text-lg">Данные:</h4>
            <div
              class="text-sm text-gray-500 hover:underline hover:text-gray-600 cursor-pointer"
              @click="isOpenModal = true"
            >
              Изменить
            </div>
          </div>
          <ul class="list-none">
            <li v-for="paramCaption,key in params" class="overflow-hidden text-ellipsis whitespace-nowrap">
              <span class="font-bold">{{ paramCaption }}: </span>
              {{ customer[key] }}
            </li>
          </ul>
        </div>
      </div>
      <div class="grow pl-6">
        <h4 class="flex items-baseline text-xl mb-3">
          <div>Взятые книги:</div>
        </h4>
      </div>
    </div>
    <div
      v-else
      class="absolute top-0 bottom-0 left-0 right-0 flex justify-center items-center h-full"
    >
      Выберите клиента для просмотра
    </div>

    <CustomerModal
      v-model="isOpenModal"
      :initialObject="customer"
      :key="(customer?.id as any)"
    />
  </UCard>
</template>