import { EndpointBuilder, ReducerPathFrom, TagTypesFromApi } from "@reduxjs/toolkit/dist/query/endpointDefinitions";
import { BaseQueryApi, createApi, fetchBaseQuery } from "@reduxjs/toolkit/dist/query/react";
import { ITask } from "../models/ITask";

export const taskAPI = createApi({
    reducerPath: 'taskAPI',
    baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5556/api/tasks/' }),
    endpoints: (build) => ({
        fetchAllTasks: build.query < ITask[], number>({
            query: (limit: number = 5) => ({
                url: '/all',
                params: {
                    _limit: limit
                }
            })
        })
    })
})