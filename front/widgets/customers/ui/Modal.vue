<script setup lang="ts">
import { object, string, type InferType } from 'yup';
import {
  type Customer,
  type CreatedCustomer,
  createCustomer
} from '~/entities/customer';
import { editCustomer } from '~/entities/customer/api/edit';
import type { EditedCustomer } from '~/entities/customer/types';

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
  (event: 'update:modelValue', value: boolean) : void
  (event: 'updateData') : void
}>();

watch(() => props.modelValue, (newVal) => emit('update:modelValue', newVal));

const customer = ref<CreatedCustomer|EditedCustomer>({
  name: props.initialObject?.name ?? '',
  address: props.initialObject?.address ?? '',
  zip: props.initialObject?.zip ?? '',
  city: props.initialObject?.city ?? '',
  phone: props.initialObject?.phone ?? '',
  email: props.initialObject?.email ?? '',
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

const errorMessage = ref();

const inputValueChange = () => {
  console.log('inp');
  errorMessage.value = '';
}

const handleSubmit = () => {
  if (!schema.isValidSync(customer.value)) {
    schema.validateSync(customer.value, { abortEarly: false });
    return;
  }

  const fn = !props.initialObject?.id
    ? () => createCustomer(customer.value)
    : () => editCustomer(props.initialObject!.id, customer.value);

  fn()
    .then(res => {
      if (!res._data) return;
      if (res.statusText !== "OK" || typeof res._data == "string") {
        errorMessage.value = res._data;
        return;
      }
      // temporary
      if (props.initialObject) {
        props.initialObject!.name = res._data.name;
        props.initialObject!.email = res._data.email;
        props.initialObject!.phone = res._data.phone;
        props.initialObject!.address = res._data.address;
        props.initialObject!.zip = res._data.zip;
        props.initialObject!.city = res._data.city;
      }
      localModel.value = false;
    })
}
</script>

<template>
  <UModal
    v-model="localModel"
  >
    <UCard>
      <UForm
        :schema="schema"
        :state="customer!"
        class="space-y-4"
        @submit="handleSubmit"
      >
        <UFormGroup
          v-for="(label, key) in fields"
          :label="label"
          :name="key"
        >
          <UInput
            v-model="(customer![key] as string)"
            @update:modelValue="inputValueChange"
          />
        </UFormGroup>

        <p
          v-if="errorMessage"
          class="text-center text-red-500 text-sm"
        >
          {{ errorMessage }}
        </p>
        
        <UButton type="submit" class="w-full justify-center">
          Сохранить
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>