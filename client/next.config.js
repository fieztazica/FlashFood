/** @type {import('next').NextConfig} */

// TODO: replace with production URL of .NET App
const DEPLOY_API = 'https://server20230321160155.azurewebsites.net' // e.g. 'https://nextjs-api.jamstacks.net'
const USE_DEV_PROXY = false // Use CORS-free URL: http://localhost:3000/api
const DEV_API = 'https://localhost:44375'

const isProd = process.env.NODE_ENV === 'production'

const buildLocal = process.env.MODE === 'local'
const API_URL = isProd ? DEPLOY_API : (USE_DEV_PROXY || buildLocal ? '' : DEV_API)

const nextConfig = {
    reactStrictMode: true,
    async rewrites() {
        let rules = [];
        if (!isProd) {
            //process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0"; // when https works https://github.com/vercel/next.js/issues/21537
            rules.push({
                source: '/api/:path*',
                destination: 'http://localhost:55468/api/:path*',
            });
        }
        return rules;
    },

    env: {
        apiBaseUrl: API_URL
    },
}

module.exports = nextConfig
