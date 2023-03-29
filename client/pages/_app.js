import { ChakraProvider } from '@chakra-ui/react'
import { extendTheme } from '@chakra-ui/react'
import { AppContextProvider } from '../lib/AppContext'
import Head from "next/head";

const colors = {
    brand: {
        900: '#1a365d',
        800: '#153e75',
        700: '#2a69ac',
    },
}

const theme = extendTheme({ colors })

export default function App({ Component, pageProps }) {
    const getLayout = Component.getLayout || ((page) => page);

    return (
        <>
            <Head>
                <title>FlashFood</title>
                <meta name="title" content={"FlashFood"} />
                <meta
                    name="description"
                    content={`Super Speed Food Service`}
                />
                <meta content="width=device-width, initial-scale=1" name="viewport" />
                <meta name="language" content="English" />
                <meta name="robots" content="index, follow" />
                <meta httpEquiv="Content-Type" content="text/html; charset=utf-8" />
            </Head>
            <ChakraProvider theme={theme}>
                <AppContextProvider>
                    {getLayout(<Component {...pageProps} />)}
                </AppContextProvider>
            </ChakraProvider>
        </>
    )
}
