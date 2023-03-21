import { createContext, useState } from "react"
import useSWR from "swr"

export const Routes = {
    /**
     * 
     * @param {string} redirectTo
     * @returns
     */
    signin: (redirectTo = null) => redirectTo ? `/signin?redirect=${redirectTo}` : `/signin`,
    forbidden: () => '/forbidden',
}