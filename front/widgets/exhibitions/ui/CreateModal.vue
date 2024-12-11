<script setup lang="ts">
import { object, string } from 'yup';
import { createExhibition } from '~/entities/exhibition/api/create';
import { useExhibitionsStore } from '~/entities/exhibition/store';
import type { CreatedExhibition } from '~/entities/exhibition/types';

const exStore = useExhibitionsStore();

const model = defineModel<boolean>();

const emit = defineEmits<{
  (event: 'update:modelValue', value: boolean) : void
}>();

const exhibition = ref<CreatedExhibition>({
  name: '',
  description: '',
});

const requiredString = "Обязательное поле";

const schema = object({
  name: string().required(requiredString),
  description: string().required(requiredString),
})

const fields = ref<{[key in keyof CreatedExhibition]: string}>({
  name: "Имя",
  description: "Описание",
});

const errorMessage = ref();

const inputValueChange = () => {
  errorMessage.value = '';
}

const handleSubmit = () => {
  if (!schema.isValidSync(exhibition.value)) {
    schema.validateSync(exhibition.value, { abortEarly: false });
    return;
  }

  exStore.create(exhibition.value).then(res => {
    if (res) model.value = false;
  })
}
</script>

<template>
  <UModal
    v-model="model"
  >
    <UCard>
      <h2 class="text-xl mb-4">Создать выставку</h2>
      <UForm
        :schema="schema"
        :state="exhibition!"
        class="space-y-4"
        @submit="handleSubmit"
      >
        <UFormGroup
          v-for="(label, key) in fields"
          :label="label"
          :name="key"
        >
          <UInput
            v-if="key === 'name'"
            v-model="(exhibition![key] as string)"
            @update:modelValue="inputValueChange"
          />
          <UTextarea
            v-else
            v-model="(exhibition![key] as string)"
            @update:modelValue="inputValueChange"
          />
        </UFormGroup>

        <p
          v-if="errorMessage"
          class="text-center text-red-500 text-sm"
        >
          {{ errorMessage }}
        </p>
        
        <UButton type="submit" class="w-full justify-center py-2 mt-2">
          Сохранить
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>