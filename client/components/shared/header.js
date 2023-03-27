import { ReactNode } from 'react'
import {
    Box,
    Flex,
    Avatar,
    HStack,
    IconButton,
    Link,
    Button,
    Menu,
    MenuButton,
    MenuList,
    MenuItem,
    MenuDivider,
    useDisclosure,
    useColorModeValue,
    Stack,
    Spacer,
    Switch,
} from '@chakra-ui/react'
import { HamburgerIcon, CloseIcon } from '@chakra-ui/icons'
import react from 'react'
import { ShoppingCart } from 'phosphor-react'
import NextLink from 'next/link'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
const Links = ['Home', 'Account', 'Contact', 'Sign In', 'Sign Up']

const NavLink = ({ href, children }) => (
    <Link
        px={2}
        py={1}
        rounded={'md'}
        _hover={{
            textDecoration: 'none',
            bg: useColorModeValue('gray.200', 'gray.700'),
        }}
        as={NextLink}
        href={href}
    >
        {children}
    </Link>
)

export default function Simple() {
    const { isOpen, onOpen, onClose } = useDisclosure()

    return (
        <>
            <Box padding={[0, 4.8]}>
                <Flex
                    h={16}
                    alignItems={'center'}
                    justifyContent={'space-between'}
                >
                    <IconButton
                        size={'md'}
                        icon={isOpen ? <CloseIcon /> : <HamburgerIcon />}
                        aria-label={'Open Menu'}
                        display={{ md: 'none' }}
                        onClick={isOpen ? onClose : onOpen}
                    />

                    <Box color={'#333'} fontSize={30}>
                        FlashFood
                    </Box>
                    <Flex alignItems={'center'}>
                        <HStack
                            as={'nav'}
                            spacing={4}
                            display={{ base: 'none', md: 'flex' }}
                            color={'#333'}
                            fontWeight={100}
                        >
                            {Links.map((link) => (
                                <NavLink key={link} href={`/${link}`}>
                                    {link}
                                </NavLink>
                            ))}
                            <NavLink href="/shop">Shop</NavLink>
                            <NavLink href={'/cart'}>
                                <ShoppingCart size={32} />
                            </NavLink>
                        </HStack>
                    </Flex>
                </Flex>
                {isOpen ? (
                    <Box pb={4} display={{ md: 'none' }}>
                        <Stack as={'nav'} spacing={4}>
                            {Links.map((link) => (
                                <NavLink key={link}>{link}</NavLink>
                            ))}
                        </Stack>
                    </Box>
                ) : null}
            </Box>
        </>
    )
}
