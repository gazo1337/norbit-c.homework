import { EndpointBuilder, ReducerPathFrom, TagTypesFromApi } from "@reduxjs/toolkit/dist/query/endpointDefinitions";
import { BaseQueryApi, createApi, fetchBaseQuery } from "@reduxjs/toolkit/dist/query/react";
import { IUser } from "../models/IUser";

export const userAPI = createApi({
    reducerPath: 'userAPI',
    baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5555/user/' }),
    endpoints: (build) => ({
        loginUser: build.mutation<IUser, IUser>({
            query: (user) => ({
                url: '/login',
                method: 'POST',
                body: user
            })
        })
    })
})