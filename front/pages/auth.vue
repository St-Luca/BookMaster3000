<script lang="ts" setup>
import { login } from '~/features/auth/api/login';
import type { AuthForm } from '~/features/auth/types';

const router = useRouter();

definePageMeta({
  layout: "empty",
});

const authData = ref<AuthForm>({
  username: "",
  password: "",
});

const fields = ref<{[key in keyof AuthForm]: string}>({
  username: "Имя",
  password: "Пароль"
});

const errorMessage = ref();

const inputValueChange = () => {
  errorMessage.value = '';
}

const handleSubmit = () => {
  login(authData.value).then(res => {
    if (res) {
      router.push('/circulation');
    }
    else {
      errorMessage.value = 'Неверные данные';
    }
  })
}
</script>

<template>
  <div class="h-[100vh] flex items-center justify-center">
    <UCard class="w-[500px] max-w-[90%]">
      <UForm
        :state="authData"
        class="flex flex-col items-center"
        @submit="handleSubmit"
      >
        <h1 class="text-lg">Вход</h1>
        <div class="w-full space-y-4 mt-2">
          <UFormGroup
            v-for="(label, key) in fields"
            :label="label"
            :name="key"
            class="w-full"
          >
            <UInput
              v-model="(authData[key] as string)"
              :type="key === 'password' ? 'password' : 'text'"
              @update:modelValue="inputValueChange"
            />
          </UFormGroup>
  
          <p
            v-if="errorMessage"
            class="text-center text-red-500 text-sm"
          >
            {{ errorMessage }}
          </p>
        </div>
        
        <UButton type="submit" class="w-full justify-center !mt-6 py-2">
          Войти
        </UButton>
      </UForm>
    </UCard>
  </div>
</template>