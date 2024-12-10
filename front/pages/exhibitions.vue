<script lang="ts" setup>
import { getExhibitionsList } from '~/entities/exhibition/api/get-list';
import ExhibitionCard from '~/widgets/exhibitions/ui/ExhibitionCard.vue';

const { data, status } = useAsyncData(async () => {
  const list = await getExhibitionsList();
  return list;
}, { immediate: true })

definePageMeta({
  name: "exhibitions",
  layout: 'full-height',
  middleware: 'auth',
});
</script>

<template>
  <UCard
    :ui="{
      body: {
        base: 'h-full',
      },
    }"
  >
    <div v-if="!!data && data.exhibitions.length">
      <h1 class="text-xl font-medium mb-3">Выставки</h1>
      <div class="divide-y divide-gray-300">
        <ExhibitionCard
          v-for="ex in data.exhibitions"
          :data="ex"
        ></ExhibitionCard>
      </div>
    </div>
    <div v-else class="h-full text-xl flex items-center justify-center">
      Выставок не найдено
    </div>
  </UCard>
</template>
