<script lang="ts" setup>
import { useAuthStore } from '~/features/auth/store';
import { CustomerModal } from '~/widgets/customers';
import { CreateExhibitionModal } from '~/widgets/exhibitions';

const authStore = useAuthStore();

const router = useRouter();

interface MenuItem {
  label: string;
  to?: string;
  dropdown?: MenuItem[][];
  click?: () => void;
  authOnly?: boolean;
  noAuthOnly?: boolean;
  class?: string;
}

const items = ref<Record<'left'|'right', Array<MenuItem>>>({
  left: [
    {
      label: "Книги",
      to: "/browse-books",
      // authOnly: true
    },
    {
      label: "Клиенты",
      dropdown: [[
        {
          label: "Найти",
          to: "/browse-customers",
          class: "bg-white hover:bg-gray-50"
        },
        {
          label: "Создать",
          click: () => isOpenCustomerModal.value = true,
          class: "bg-white hover:bg-gray-50"
        },
      ]],
      authOnly: true
    },
    {
      label: "Циркуляция",
      to: "/circulation",
      authOnly: true
    },
    {
      label: "Выставки",
      to: "/exhibitions",
      noAuthOnly: true
    },
    {
      label: "Выставки",
      authOnly: true,
      dropdown: [[
        {
          label: "Просмотр",
          to: "/exhibitions",
          class: "bg-white hover:bg-gray-50"
        },
        {
          label: "Создать",
          click: () => isOpenExhibitionModal.value = true,
          class: "bg-white hover:bg-gray-50"
        },
      ]],
    },
  ],
  right: [
    {
      label: "Выйти",
      click: () => {
        authStore.logout();
        router.push("/")
      },
      authOnly: true
    },
    {
      label: "Войти",
      to: "/auth",
      noAuthOnly: true
    }
  ]
});

const isOpenCustomerModal = ref(false);
const isOpenExhibitionModal = ref(false);

const filteredItems = (items:Array<MenuItem>) => {
  return items.filter(item => 
    (!item.authOnly || authStore.isAuth) && (!item.noAuthOnly || !authStore.isAuth)
  );
}

</script>

<template>
  <header>
    <UCard
      :ui="{
        body: {
          padding: '!py-2',
        },
      }"
    >
      <template #default>
        <nav class="flex justify-between">
          <div v-for="itemsOnSide in [items.left, items.right]" class="flex gap-2">
            <template v-for="item in filteredItems(itemsOnSide)">
              <UDropdown
                v-if="item.dropdown"
                :items="item.dropdown"
              >
                <UButton
                  :variant="'ghost'"
                  :label="item.label"
                  color="black"
                  class="hover:bg-gray-100"
                />
                <template #item="{ item }">
                  <span @click="() => { if (item.click) item.click() }">{{ item.label }}</span>
                </template>
              </UDropdown>
              <UButton
                v-else
                :variant="'ghost'"
                :label="item.label"
                :to="item.to"
                color="black"
                class="hover:bg-gray-100"
                @click="() => { if (item.click) item.click() }"
              />
            </template>
          </div>
        </nav>
      </template>
    </UCard>

    <CustomerModal v-model="isOpenCustomerModal"></CustomerModal>
    <CreateExhibitionModal v-model="isOpenExhibitionModal" />
  </header>
</template>