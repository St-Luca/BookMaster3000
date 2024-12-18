<script lang="ts" setup>
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
    <div v-if="exhibitionsStore.list.length" class="h-full">
      <h1 class="text-xl font-medium mb-3">Выставки</h1>
      <div class="divide-y divide-gray-300">
        <ExhibitionCard
          v-for="ex in exhibitionsStore.list"
          :data="ex"
          :key="ex.id"
        ></ExhibitionCard>
      </div>
    </div>
    <div v-else class="h-full text-xl flex items-center justify-center">
      Выставок не найдено
    </div>
  </UCard>
</template>
