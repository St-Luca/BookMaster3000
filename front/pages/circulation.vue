<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import BrowseLayout from '~/features/browse/ui/BrowseLayout.vue';
import CirculationCard from '~/widgets/circulation/ui/CirculationCard.vue';
import CirculationSearchParams from '~/widgets/circulation/ui/CirculationSearchParams.vue';

definePageMeta({
  layout: 'full-height'
});

const searchDone = ref(false);

const currentCustomer = ref<Customer|undefined>(undefined);

const handleSearchResult = (res?:Customer) => {
  searchDone.value = true;
  currentCustomer.value = res;
}
</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <CirculationSearchParams
        class="h-full"
        @result="handleSearchResult"
      />
    </template>
    <template #top>
      <CirculationCard
        v-if="currentCustomer"
        :customer="currentCustomer"
        class="h-full"
      />
      <UCard
        v-else="!searchDone"
        class="h-full text-slate-700"
        :ui="{ body: { base: 'h-full flex justify-center items-center' } }"
      >
        <div class="inline" v-if="!searchDone">Введите ID клиента для просмотра циркуляции</div>
        <div class="inline" v-else>Клиент не найден</div>
      </UCard>
    </template>
  </BrowseLayout>
</template>