<script lang="ts" setup>
import type { Book } from '~/entities/book';
import type { Customer } from '~/entities/customer';
import { BrowseLayout, BrowseList } from '~/features/browse';
import { CustomerSearchParams } from '~/widgets/browse-customers';
import CustomerCard from '~/widgets/browse-customers/ui/CustomerCard.vue';

definePageMeta({
  layout: 'full-height'
})

const mockupList = ref<Customer[]>([
  {
    id: '1',
    name: "Иван Иванов",
    address: "ул. Ленина, д. 123",
    zip: "101000",
    city: "Москва",
    phone: "+7 495 123-4567",
    email: "ivan.ivanov@example.com",
  },
  {
    id: '2',
    name: "Мария Петрова",
    address: "пр. Невский, д. 456",
    zip: "191000",
    city: "Санкт-Петербург",
    phone: "+7 812 987-6543",
    email: "maria.petrova@example.com",
  },
  {
    id: '3',
    name: "Сергей Сидоров",
    address: "ул. Баумана, д. 789",
    zip: "400001",
    city: "Казань",
    phone: "+7 843 555-6677",
    email: "sergey.sidorov@example.com",
  },
  {
    id: '4',
    name: "Анна Смирнова",
    address: "ул. Северная, д. 101",
    zip: "620014",
    city: "Екатеринбург",
    phone: "+7 343 999-1122",
    email: "anna.smirnova@example.com",
  },
  {
    id: '5',
    name: "Дмитрий Кузнецов",
    address: "ул. Южная, д. 202",
    zip: "354000",
    city: "Сочи",
    phone: "+7 862 777-2233",
    email: "dmitry.kuznetsov@example.com",
  },
  {
    id: '6',
    name: "Елена Орлова",
    address: "ул. Центральная, д. 303",
    zip: "660000",
    city: "Красноярск",
    phone: "+7 391 888-3344",
    email: "elena.orlova@example.com",
  },
  {
    id: '7',
    name: "Михаил Воробьев",
    address: "пр. Свободы, д. 404",
    zip: "680000",
    city: "Хабаровск",
    phone: "+7 4212 999-4455",
    email: "mikhail.vorobiev@example.com",
  },
  {
    id: '8',
    name: "Ольга Соколова",
    address: "ул. Молодежная, д. 505",
    zip: "630000",
    city: "Новосибирск",
    phone: "+7 383 222-5566",
    email: "olga.sokolova@example.com",
  },
  {
    id: '9',
    name: "Алексей Морозов",
    address: "ул. Пушкина, д. 606",
    zip: "443000",
    city: "Самара",
    phone: "+7 846 333-6677",
    email: "alexey.morozov@example.com",
  },
  {
    id: '10',
    name: "Наталья Федорова",
    address: "ул. Гагарина, д. 707",
    zip: "394000",
    city: "Воронеж",
    phone: "+7 473 444-7788",
    email: "natalya.fedorova@example.com",
  },
]);

const viewedCustomer = ref<Customer|undefined>(undefined);

const handleBookSelect = (customer: Customer) => {
  viewedCustomer.value = customer;
}

const cols = ref([
  {
    label: 'ID',
    key: 'id'
  },
  {
    label: 'Имя',
    key: 'name'
  }
])

const rows = (customer:Customer) => ({
  ref: customer,
  id: customer.id,
  name: customer.name,
})

</script>

<template>
  <BrowseLayout>
    <template #sidebar>
      <CustomerSearchParams class="h-full" />
    </template>
    <template #top>
      <BrowseList
        :list="mockupList"
        :rows="rows"
        :cols="cols"
        :highlightedItem="viewedCustomer"
        @select="handleBookSelect"
        class="h-full" 
      />
    </template>
    <template #bottom>
      <CustomerCard
        :customer="viewedCustomer"
        class="h-full"
      />
    </template>
  </BrowseLayout>
</template>