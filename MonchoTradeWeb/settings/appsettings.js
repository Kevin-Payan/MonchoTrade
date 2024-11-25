export const appsettings = {
<<<<<<< Updated upstream
    apiUrl : 'http://localhost:5121',
=======
    apiUrl : 'https://webapp-monchotrade-cbbee3h5chdhf5d3.centralus-01.azurewebsites.net',
>>>>>>> Stashed changes
    sessionRoute : '/session',
    profileImageRoute: '/profileimage', 
    productRoute: '/product',
<<<<<<< Updated upstream
}
=======
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
>>>>>>> Stashed changes
