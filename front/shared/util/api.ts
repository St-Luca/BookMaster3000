type FetchRawType<T> = ReturnType<typeof $fetch.raw<T>>;

export const api = <T>(
  path: string,
  params: {
    method: "GET"|"POST"|"PUT"|"DELETE",
    body?: Record<string, any>,
    headers?: { [key:string]: string }
  }
) : FetchRawType<T> => {
  const config = useRuntimeConfig();
  
  return $fetch.raw<T>(
    `${config.public.baseApiUrl}/api${path[0] == '/' ? '' : '/'}${path}`,
    {
      ...params,
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        ...params.headers,
      },
      ignoreResponseError: true
    }
  );
}