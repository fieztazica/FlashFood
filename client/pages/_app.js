import { ChakraProvider } from '@chakra-ui/react'
import { extendTheme } from '@chakra-ui/react'
import { AppContextProvider } from '../lib/AppContext'

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
            <ChakraProvider theme={theme}>
                <AppContextProvider>
                    {getLayout(<Component {...pageProps} />)}
                </AppContextProvider>
            </ChakraProvider>
        </>
    )
}
