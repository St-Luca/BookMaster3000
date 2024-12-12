<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import { getCustomer } from '~/entities/customer/api/get';
import BrowseLayout from '~/features/browse/ui/BrowseLayout.vue';
import CirculationCard from '~/widgets/circulation/ui/CirculationCard.vue';
import CirculationSearchParams from '~/widgets/circulation/ui/CirculationSearchParams.vue';

definePageMeta({
  layout: 'full-height',
  middleware: 'auth',
});

const searchDone = ref(false);

const currentCustomer = ref<Customer|undefined>(undefined);

const lastCustomerId = ref<string>('');
const key = ref(0)

const handleSearchResult = (id?:string) => {
  getCustomer(id ?? lastCustomerId.value).then(res => {
    if (res) {
      console.log('handleSearchResult', id, lastCustomerId.value);
      lastCustomerId.value = id ?? lastCustomerId.value;
      searchDone.value = true;
      currentCustomer.value = res;
      key.value++;
    }
  })
}
</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <CirculationSearchParams
        class="h-full"
        @submit="handleSearchResult"
      />
    </template>
    <template #top>
      <CirculationCard
        v-if="currentCustomer"
        :customer="currentCustomer"
        :key="key"
        class="h-full"
        @refresh="handleSearchResult"
      />
      <UCard
        v-else
        class="h-full text-slate-700"
        :ui="{ body: { base: 'h-full flex justify-center items-center' } }"
      >
        <div class="inline" v-if="!searchDone">Введите ID клиента для просмотра циркуляции</div>
        <div class="inline" v-else>Клиент не найден</div>
      </UCard>
    </template>
  </BrowseLayout>
</template>
