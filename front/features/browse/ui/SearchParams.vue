<script lang="ts" setup generic="TModel extends Record<string, any>">
import { searchBook } from '~/entities/book/api/search';
import { type Book, type BookSearchParams } from '~/entities/book/types';

const props = defineProps<{
  modelValue: TModel,
  fieldNames: {[key in keyof TModel]: string},
}>();

const localModel = computed({
  get: () => props.modelValue,
  set: (value) => {
    emit('update:modelValue', value);
  },
})

const emit = defineEmits<{
  (event: 'submit', value: TModel): void,
  (event: 'update:modelValue', value: TModel): void,
}>();

const handleSubmit = () => emit('submit', props.modelValue);
</script>

<template>
  <UCard>
    <UForm
      :state="localModel"
      class="grid grid-cols-[auto_1fr] gap-4 items-baseline"
      @submit="handleSubmit"
    >
      <template v-for="label, key in fieldNames">
        <div>{{label}}</div>
        <UInput v-model="localModel[key]"></UInput>
      </template>
      <div></div>
      <UButton type="submit" class="mt-2 justify-center">
        <template #leading>
          <UIcon name="i-heroicons-outline-magnifying-glass" class="w-3 h-3" />
        </template>
        Поиск
      </UButton>
    </UForm>
  </UCard>
</template>