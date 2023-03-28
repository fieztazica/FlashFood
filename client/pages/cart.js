import Head from 'next/head'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image } from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'

function Cart() {
    const { user, cart } = useAppStates();
    return (
        <>
            <Box>
                
            </Box>
            <Box>

            </Box>
        </>
    )
}

Cart.getLayout = function getLayout(page) {
    return (
        <AppLayout>
            {page}
        </AppLayout>
    )
}

export default Cart
