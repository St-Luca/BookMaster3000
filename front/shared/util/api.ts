export const api = <T>(
  path: string,
  params: {
    method: "GET"|"POST"|"PUT"|"DELETE",
    body: Record<string, any>,
    headers?: { [key:string]: string }
  }
) => {
  const config = useRuntimeConfig();
  
  return $fetch<T>(
    `${config.public.baseApiUrl}${path[0] == '/' ? '' : '/'}${path}`,
    {
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        ...params.headers,
      },
      ...params
    }
  ).then(res => {console.log(res); return res})
} 