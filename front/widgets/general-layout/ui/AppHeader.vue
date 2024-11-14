<script lang="ts" setup>
import { useAuthStore } from '~/features/auth/store';
import { CustomerModal } from '~/widgets/customers';

const authStore = useAuthStore();

const router = useRouter();

interface MenuItem {
  label: string;
  to?: string;
  dropdown?: MenuItem[][];
  click?: () => void;
  class?: string;
}

const items = ref<Record<string, Array<MenuItem>>>({
  left: [
    {
      label: "Книги",
      to: "/browse-books"
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
      ]]
    },
    {
      label: "Циркуляция",
      to: "/circulation"
    },
  ],
  right: [
    {
      label: "Выйти",
      click: () => {
        authStore.logout();
        router.push("/auth")
      },
    }
  ]
});

const isOpenCustomerModal = ref(false);

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
          <div v-for="itemsOnSide in items" class="flex gap-2">
            <template v-for="item in itemsOnSide">
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
  </header>
</template>