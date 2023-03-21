import { Box, HStack } from '@chakra-ui/react'
import NextLink from 'next/link'

const navLinks = [
    {
        name: 'trang chu',
        link: '/home',
    },
    {
        name: 'tai khoan',
        link: '/account',
    },
    {
        name: 'chinh sach',
        link: '/policy',
    },
]

function Header() {
    return (
        <>
            <HStack>
                {navLinks.map((navLink) => (
                    <Box as={NextLink} key={navLink.link} href={navLink.link}>
                        {navLink.name}
                    </Box>
                ))}
            </HStack>
        </>
    )
}

export default Header
