<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import { BrowseLayout, BrowseList } from '~/features/browse';
import { CustomerSearchParams, CustomerCard } from '~/widgets/customers';

definePageMeta({
  layout: 'full-height',
  middleware: 'auth',
})

const customersList = ref<Customer[]|undefined>(undefined);

const viewedCustomer = ref<Customer|undefined>(undefined);

const handleBookSelect = (customer: Customer) => {
  viewedCustomer.value = customer;
}

const cols = ref([
  {
    label: 'ID',
    key: 'id'
  },
  {
    label: 'Имя',
    key: 'name'
  }
])

const rows = (customer:Customer) => ({
  ref: customer,
  id: customer.id,
  name: customer.name,
})

const page = ref(1);
const totalItems = ref(0);

const handleSearchResult = (res: Customer[]) => {
  customersList.value = res;
  if (!customersList.value.find(b => b.id == viewedCustomer.value?.id)) {
    viewedCustomer.value = undefined;
  }
}
</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <CustomerSearchParams
        class="h-full"
        :page="page"
        @result="handleSearchResult"
      />
    </template>
    <template #top>
      <BrowseList
        :list="customersList"
        :rows="rows"
        :cols="cols"
        v-model:page="page"
        :totalItems="totalItems"
        :highlightedItem="viewedCustomer"
        @select="handleBookSelect"
        class="h-full" 
      />
    </template>
    <template #bottom>
      <CustomerCard
        :customer="viewedCustomer"
        class="h-full"
      />
    </template>
  </BrowseLayout>
</template>