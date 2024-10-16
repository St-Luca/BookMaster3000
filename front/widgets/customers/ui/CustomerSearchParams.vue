<script lang="ts" setup>
import type { Customer } from '~/entities/customer';
import { getCustomer } from '~/entities/customer/api/get';
import { searchCustomers } from '~/entities/customer/api/search';
import type { CustomerSearchParams } from '~/entities/customer/types';
import { SearchParams } from '~/features/browse';

const emit = defineEmits<{
  (event: 'result', value: Customer[]): void
}>();

const searchParams = ref<CustomerSearchParams>({
  id: "",
  name: "",
});

const fieldNames = ref<{[key in keyof CustomerSearchParams]: string}>({
  id: "ID",
  name: "Имя",
});

const handleSubmit = () => {
  if (searchParams.value.id) {
    getCustomer(searchParams.value.id)
      .then(res => emit('result', [res]))
      .catch(err => emit('result', []));
  }
  else if (searchParams.value.name) {
    searchCustomers(searchParams.value.name)
      .then(res => emit('result', res))
      .catch(err => emit('result', []));
  }
};
</script>

<template>
  <SearchParams
    :modelValue="searchParams"
    :fieldNames="fieldNames"
    @submit="handleSubmit"
  ></SearchParams>
</template>