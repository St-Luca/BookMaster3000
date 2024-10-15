<script setup lang="ts">
import { object, string, type InferType } from 'yup';
import {
  type Customer,
  type CreatedCustomer,
  createCustomer
} from '~/entities/customer';

const props = defineProps<{
  modelValue: boolean,
  initialObject?: Customer,
}>();

const localModel = computed({
  get: () => props.modelValue,
  set: (value) => {
    emit('update:modelValue', value);
  },
})

const emit = defineEmits<{
  (event: 'update:modelValue', value: boolean): void
}>();

watch(() => props.modelValue, (newVal) => emit('update:modelValue', newVal));

const customer = ref<Customer>();

onBeforeMount(() => {
  customer.value = {
    id: props.initialObject?.name ?? '',
    name: props.initialObject?.name ?? '',
    address: props.initialObject?.address ?? '',
    zip: props.initialObject?.zip ?? '',
    city: props.initialObject?.city ?? '',
    phone: props.initialObject?.phone ?? '',
    email: props.initialObject?.email ?? '',
  };
});

const requiredString = "Обязательное поле";

const schema = object({
  name: string().required(requiredString),
  phone: string().required(requiredString),
  email: string().email('Некорректный E-mail').required(requiredString),
})

const fields = ref<{[key in keyof CreatedCustomer]: string}>({
  name: "Имя",
  phone: "Телефон",
  email: "E-mail",
  city: "Город",
  address: "Адрес",
  zip: "Почтовый индекс",
});

const handleSubmit = () => {
  if (!schema.isValidSync(customer.value)) {
    schema.validateSync(customer.value, { abortEarly: false });
    return;
  }
  createCustomer(customer.value);
}

</script>

<template>
  <UModal
    v-model="localModel"
  >
    <UCard>
      <UForm :schema="schema" :state="customer!" class="space-y-4" @submit="handleSubmit">
        <UFormGroup v-for="label,key in fields" :label="label" :name="key">
          <UInput v-model="(customer![key] as string)" />
        </UFormGroup>
  
        <UButton type="submit" class="w-full justify-center">
          Сохранить
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>