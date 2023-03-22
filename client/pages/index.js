import Head from 'next/head'
import Image from 'next/image'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading } from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'

function Home() {
    const { user } = useAppStates();
    return (
        <>
            <Box>
                <Heading>
                    {user?.["Email"]}
                </Heading>
                <Button>
                    nut
                </Button>
            </Box>
        </>
    )
}

Home.getLayout = function getLayout(page) {
    return (
        <AppLayout>
            {page}
        </AppLayout>
    )
}

export default Home
