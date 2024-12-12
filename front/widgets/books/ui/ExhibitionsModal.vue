<script setup lang="ts">
import type { Book } from '~/entities/book';
import type { Exhibition } from '~/entities/exhibition';
import { useExhibitionsStore } from '~/entities/exhibition/store';

const exhibitionsStore = useExhibitionsStore();

const { status } = useAsyncData(async () => {
  exhibitionsStore.fetch();
}, { immediate: true })

const model = defineModel<boolean>();

const props = defineProps<{
  book: Book,
}>();

const hasBook = (ex:Exhibition) => {
  return exhibitionsStore.hasBook(ex.id, props.book.id);
}

const pendingArr = ref<Array<string|number>>([]);

const toggleBook = async (ex:Exhibition) => {
  pendingArr.value.push(ex.id);
  exhibitionsStore.toggleBook(props.book, ex);
  pendingArr.value = pendingArr.value.filter(id => id !== ex.id);
}
</script>

<template>
  <UModal v-model="model">
    <UCard
      class="max-h-[75vh] h-[75vh] overflow-hidden"
      :ui="{
        body: {
          base: 'h-full overflow-hidden',
        },
      }"
    >
      <div class="h-full flex flex-col justify-between overflow-hidden min-h-0">
        <h2 class="text-xl mb-3">Настроить выставки</h2>
        <div class="pr-1.5 grow min-h-0 overflow-y-auto scrollbar-w-[3px] scrollbar scrollbar-thumb-slate-400 scrollbar-thumb-rounded-[4px]">
          <div class="flex flex-col divide-y divide-gray-200">
            <div
              v-for="ex in exhibitionsStore.list"
              class="flex items-center justify-between py-2 first:pt-1"
            >
              <p>{{ ex.name }}</p>
              <UButton
                class="w-[100px] justify-center duration-150"
                :class="{ 'hover:bg-red-50 bg-transparent': hasBook(ex) }"
                :color="!hasBook(ex) ? 'primary': 'red'"
                variant="soft"
                :loading="pendingArr.includes(ex.id)"
                :disabled="pendingArr.includes(ex.id)"
                @click="toggleBook(ex)"
              >
                {{ !hasBook(ex) ? 'Добавить' : 'Убрать' }}
              </UButton>
            </div>
          </div>
        </div>
        <UButton class="justify-center py-2 mt-4" @click="model = false">Готово</UButton>
      </div>
    </UCard>
  </UModal>
</template>