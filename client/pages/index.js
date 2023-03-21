import Head from 'next/head'
import Image from 'next/image'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading } from '@chakra-ui/react'

function Home() {
    return (
        <>
           <Box>
            <Heading>
                Abc 
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
