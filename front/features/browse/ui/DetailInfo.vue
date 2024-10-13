<script lang="ts" setup>
import type { Author } from '~/entities/author';
import type { Book } from '~/entities/book';

interface BadgeContainer<T> {
  name: string,
  items: Array<{
    ref: T,
    label: string,
  }>,
  clickHandler?: (item: T) => void
}

interface Props {
  title: string,
  subtitle?: string,
  badges?: Array<BadgeContainer<any>>,
  img?: string,
  noImagePolicy?: 'hide'|'blank',
}

const props = withDefaults(defineProps<Props>(), {
  noImagePolicy: 'hide',
});
</script>

<template>
  <div class="relative pt-0">
    <div class="grow">
      <h3 class="flex items-baseline gap-6 text-2xl">
        <div>{{ title }}</div>
        <slot name="title-tailing"></slot>
      </h3>
      <p
        v-if="subtitle"
        class="text-sm text-gray-700"
      >
        {{ subtitle }}
      </p>
      <div
        v-if="badges" 
        class="flex flex-col gap-2 mt-2 mb-3"
      >
        <div
          v-for="badge,i in badges.filter(b => b.items?.length)"
          :key="i"
          class="flex items-baseline gap-2"
        >
          <span class="text-sm">{{ badge.name }}: </span>
          <div>
            <UBadge
              v-for="item in badge.items"
              color="gray"
              class="mr-1 cursor-pointer"
              @click="() => { if (badge.clickHandler) badge.clickHandler(item.ref as any) }"
            >
              {{ item.label }}
            </UBadge>
          </div>
        </div>
      </div>
      <slot></slot>
    </div>

    <div class="w-[200px]" v-if="img || noImagePolicy == 'blank'">
      <div class="bg-slate-100 w-full aspect-[3/4] overflow-hidden">
        <img
          v-if="img"
          :src="img"
          alt=""
          class="object-cover w-full h-full"
        >
      </div>
    </div>
  </div>
</template>