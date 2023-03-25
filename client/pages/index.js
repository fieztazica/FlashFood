import Head from 'next/head'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image } from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'

function Home() {
    const { user } = useAppStates();
    return (
        <>
            <Box>
                <Heading>
                    {user?.["Email"]}
                </Heading>
                <Image src="https://cdn.discordapp.com/attachments/854996766154817559/1089082539056050197/image.png"></Image>
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
