<script lang="ts" setup>
import type { Exhibition } from '~/entities/exhibition';
import { useExhibitionsStore } from '~/entities/exhibition/store';
import ExhibitionCard from '~/widgets/exhibitions/ui/ExhibitionCard.vue';

const exhibitionsStore = useExhibitionsStore();

const { status } = useAsyncData(async () => {
  exhibitionsStore.fetch();
}, { immediate: true })

definePageMeta({
  name: "exhibitions",
  layout: 'full-height',
  middleware: 'auth',
});

const isOpenBookCardExModal = ref(false);
const frozenList = ref<Array<Exhibition>>([]);

const handleBookCardExhibitionsModalToggle = (val:boolean) => {
  console.log(val);
  isOpenBookCardExModal.value = val;
  if (val) {
    frozenList.value = JSON.parse(JSON.stringify(exhibitionsStore.list)) as Array<Exhibition>;
  }
}

const computedList = computed(() => {
  return isOpenBookCardExModal.value
    ? frozenList.value
    : exhibitionsStore.list;
})
</script>

<template>
  <UCard
    class="h-full"
    :ui="{
      body: {
        base: 'h-full overflow-y-scroll',
      },
    }"
  >
    <div v-if="computedList.length" class="h-full">
      <h1 class="text-xl font-medium mb-3">Выставки</h1>
      <div class="divide-y divide-gray-300">
        <ExhibitionCard
          v-for="ex in computedList"
          :data="ex"
          :key="ex.id"
          @bookCardExhibitionsModalToggle="handleBookCardExhibitionsModalToggle"
        ></ExhibitionCard>
      </div>
    </div>
    <div v-else class="h-full text-xl flex items-center justify-center">
      Выставок не найдено
    </div>
  </UCard>
</template>
