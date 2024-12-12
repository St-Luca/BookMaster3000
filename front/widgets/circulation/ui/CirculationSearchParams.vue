<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import { getCustomer } from '~/entities/customer/api/get';
import { searchCustomers } from '~/entities/customer/api/search';
import type { CustomerSearchParams } from '~/entities/customer/types';
import { SearchParams } from '~/features/browse';

const props = defineProps<{
  
}>();

const emit = defineEmits<{
  (event: 'submit', value: string): void
}>();

const searchParams = ref<Partial<CustomerSearchParams>>({
  id: "",
});

const fieldNames = ref<{[key in keyof CustomerSearchParams]?: string}>({
  id: "ID клиента",
});

const handleSubmit = () => {
  if (!searchParams.value.id) return;

  emit("submit", searchParams.value.id)
};
</script>

<template>
  <SearchParams
    :modelValue="searchParams"
    :fieldNames="fieldNames"
    @submit="handleSubmit"
  ></SearchParams>
</template>