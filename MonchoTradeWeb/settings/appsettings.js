export const appsettings = {
    apiUrl : 'https://localhost:7001',
    sessionRoute : '/session',
    profileImageRoute: '/profileimage', 
    productRoute: '/product',
    exchangeRoute: '/exchanges', 
    userRoute: '/user',
    categoriesRoute: '/categories', 
    axiosConfig: {
        headers: {
          "Content-Type": "application/json",
        },
        withCredentials: true
    },
    axiosPatchConfig: {
      headers: {
        "Content-Type": 'application/json-patch+json',
      },
      withCredentials: true
    },
    axiosMultiFormDataConfig:{
      headers: {
        'Content-Type': 'multipart/form-data'
      },
      withCredentials: true
    },
}
