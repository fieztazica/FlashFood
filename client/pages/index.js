import Head from 'next/head'
import AppLayout from '@/components/layouts/appLayout'
import {
    Box,
    Button,
    Heading,
    Image,
    Link,
    Stack,
    useDisclosure,
} from '@chakra-ui/react'
import NextLink from 'next/link'
import { useAppStates } from '../lib/AppContext'

const navs = [
    {
        text: 'Home',
        link: '/',
    },
    {
        text: 'Cart',
        link: '/cart',
    },
    {
        text: 'Order',
        link: '/orders',
    },
]

const NavLink = ({ text, link, ...props }) => (
    <Link as={NextLink} href={link} {...props}>
        {text}
    </Link>
)

function Home() {
    const { user } = useAppStates()
    const { isOpen, onOpen, onClose, onToggle } = useDisclosure()
    return (
        <>
            <Box>
                <Heading>{user?.['Email']}</Heading>
                <Image src="https://cdn.discordapp.com/attachments/854996766154817559/1089082539056050197/image.png"></Image>

                <Button
                    display={{ base: 'block', md: 'none' }}
                    onClick={onToggle}
                >
                    {isOpen ? 'Close' : 'Open'}
                </Button>
                {/* <Stack direction={{base: "column", md:"row"}}>
                    {navs.map((nav) => (
                        <NavLink
                            key={nav.text}
                            text={nav.text}
                            link={nav.link}
                        />
                    ))}
                </Stack> */}
                <Box bg={'lightblue'} display={{ base: 'none', md: 'block' }}>
                    {navs.map((nav) => (
                        <NavLink
                            key={nav.text}
                            text={nav.text}
                            link={nav.link}
                        />
                    ))}
                </Box>
                {isOpen && (
                    <Box bg={'yellow'} display={{ base: 'block', md: 'none' }}>
                        {navs.map((nav) => (
                            <Box key={nav.text}>
                                <NavLink text={nav.text} link={nav.link} />
                            </Box>
                        ))}
                    </Box>
                )}
            </Box>
        </>
    )
}

Home.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Home
