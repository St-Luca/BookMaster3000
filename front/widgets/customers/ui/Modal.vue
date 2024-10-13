<script setup lang="ts">
import { object, string, type InferType } from 'yup';
import type { Customer } from '~/entities/customer';

const props = defineProps<{
  modelValue: boolean,
  initialObject?: Customer,
}>();

const localModal = computed({
  get: () => props.modelValue,
  set: (value) => {
    emit('update:modelValue', value);
  },
})

const emit = defineEmits<{
  (event: 'update:modelValue', value: boolean): void
}>();

watch(() => props.modelValue, (newVal) => emit('update:modelValue', newVal));

const model = ref<Customer>();

onBeforeMount(() => {
  model.value = {
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
  address: string().required(requiredString),
  zip: string()
    .min(6, 'Некорректный индекс')
    .required(requiredString),
  city: string().required(requiredString),
  phone: string().required(requiredString),
  email: string().email('Некорректный E-mail').required(requiredString),
})

const fields = ref({
  name: "Имя",
  address: "Адрес",
  zip: "Почтовый индекс",
  city: "Город",
  phone: "Телефон",
  email: "E-mail",
})

</script>

<template>
  <UModal
    v-model="localModal"
  >
    <UCard>
      <UForm :schema="schema" :state="model!" class="space-y-4" @submit="">
        <UFormGroup v-for="label,key in fields" :label="label" :name="key">
          <UInput v-model="(model![key] as string)" />
        </UFormGroup>
  
        <UButton type="submit" class="w-full justify-center">
          Сохранить
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>