import axios from 'axios';
import instanceApi from '.';
const https = require('https');

/**
 * 
 * @param {string} token
 * @returns
 */
export function serverInstace(req) {
    const instance = axios.create({
        baseURL: `${process.env.apiBaseUrl}`
    });


    instance.defaults.headers.post["Content-Type"] = 'application/json';

    const api = instanceApi(instance);

    api.setTokenToInstance(token);

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