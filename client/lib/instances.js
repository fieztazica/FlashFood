import axios from 'axios';
import instanceApi from '.';
const https = require('https');

/**
 * 
 * @param {import("next").GetServerSidePropsContext} ctx
 * @returns
 */
export function authInstance(ctx) {
    const { req } = ctx
    const instance = axios.create({
        baseURL: `${process.env.apiBaseUrl}`,
        withCredentials: true,
        headers: {
            Cookie: req.headers.cookie
        },
        httpsAgent: new https.Agent({
            rejectUnauthorized: false
        })
    });

    instance.defaults.headers.post["Content-Type"] = 'application/json';

    const api = instanceApi(instance);

    return api;
}


export function publicInstance() {
    const instance = axios.create({
        baseURL: `${process.env.apiBaseUrl}`,
        httpsAgent: new https.Agent({
            rejectUnauthorized: false
        })
    });

    instance.defaults.headers.post["Content-Type"] = 'application/json';

    const api = instanceApi(instance);

    return api;
}