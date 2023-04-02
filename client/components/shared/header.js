import { ReactNode, useEffect } from 'react'
import {
    Box,
    Flex,
    IconButton,
    Link,
    Menu,
    MenuButton,
    MenuList,
    MenuItem,
    MenuDivider,
    useDisclosure,
    useColorModeValue,
    Stack,
    Heading,
    Divider,
    Spinner,
    HStack,
    Text,
    keyframes,
} from '@chakra-ui/react'
import { HamburgerIcon, CloseIcon } from '@chakra-ui/icons'
import NextLink from 'next/link'
import NavLink from '../NavLink'
import CartIconButton from '../CartIconButton'
import { useAppStates } from '../../lib/AppContext'
import { useRouter } from 'next/router'

const navLinks = [
    {
        text: 'Your Cart',
        href: '/cart',
    },
    {
        text: 'Your Orders',
        href: '/orders',
    },
]

const blinker = keyframes`
    50% {
        opacity: 0;
    }
`

const StyledNavLink = ({ href, children }) => (
    <NavLink
        px={2}
        py={1}
        rounded={'md'}
        _hover={{
            textDecoration: 'none',
            bg: useColorModeValue('gray.200', 'gray.700'),
        }}
        href={href}
    >
        {children}
    </NavLink>
)

function Header() {
    const indicator = useDisclosure();
    const router = useRouter();
    const { isOpen, onOpen, onClose } = useDisclosure();
    const { user, action } = useAppStates();
    const { logout } = action;

    useEffect(() => {
        const handleStart = (url) => {
            console.log(`Loading: ${url}`);
            indicator.onOpen();
        };

        const handleStop = () => {
            indicator.onClose();
        };

        router.events.on("routeChangeStart", handleStart);
        router.events.on("routeChangeComplete", handleStop);
        router.events.on("routeChangeError", handleStop);

        return () => {
            router.events.off("routeChangeStart", handleStart);
            router.events.off("routeChangeComplete", handleStop);
            router.events.off("routeChangeError", handleStop);
        };
    }, [router]);

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

                    <HStack as={NextLink} href="/" color={'#333'} fontSize={30}>
                        <Heading>FlashFood</Heading>
                        {indicator.isOpen ?
                            <Spinner /> :
                            <Text animation={`${blinker} 1s linear infinite`}>_</Text>}
                    </HStack>
                    <Flex alignItems={'center'}>
                        <Flex
                            as={'nav'}
                            display={{ base: 'none', md: 'flex' }}
                            color={'#333'}
                            fontWeight={100}
                            w={'full'}
                            px={4} py={3}
                            alignItems={'center'}
                        >
                            {!user ? (
                                <StyledNavLink
                                    href={`/login`}
                                >
                                    Login
                                </StyledNavLink>
                            ) : (
                                <Menu isLazy>
                                    <MenuButton as={Link} px={2}
                                        py={1}
                                        rounded={'md'}
                                        _hover={{
                                            textDecoration: 'none',
                                            bg: useColorModeValue('gray.200', 'gray.700'),
                                        }}>{user.Email}</MenuButton>
                                    <MenuList>
                                        <MenuItem>Hi, {`${user["FirstName"]}`}!</MenuItem>
                                        {navLinks.map((navLink, i) => (
                                            <NavLink
                                                key={'navlink-' + i}
                                                href={`${navLink.href}`}
                                                _hover={{
                                                    textDecoration: 'none',
                                                }}
                                            >
                                                <MenuItem>
                                                    {navLink.text}
                                                </MenuItem>
                                            </NavLink>
                                        ))}
                                        <MenuDivider />
                                        <MenuItem onClick={() => logout()}>Log out</MenuItem>
                                    </MenuList>
                                </Menu>
                            )}
                            <CartIconButton />
                        </Flex>
                        <Box display={{ base: 'flex', md: 'none' }}>
                            <CartIconButton />
                        </Box>
                    </Flex>
                </Flex>
                {isOpen ? (
                    <Box pb={4} display={{ md: 'none' }}>
                        <Stack as={'nav'} spacing={4}>

                            {!user ? (
                                <StyledNavLink
                                    href={`/login`}
                                >
                                    Login
                                </StyledNavLink>
                            ) : (
                                <>
                                    <StyledNavLink href="#">Hi, {`${user["FirstName"]}`}!</StyledNavLink>
                                    <StyledNavLink href="/">Shop</StyledNavLink>
                                    {navLinks.map((navLink, i) => (
                                        <StyledNavLink
                                            key={'navlink-' + i}
                                            href={`${navLink.href}`}
                                        >
                                            {navLink.text}
                                        </StyledNavLink>
                                    ))}
                                    <Divider />
                                    <Link px={2}
                                        py={1}
                                        rounded={'md'}
                                        _hover={{
                                            textDecoration: 'none',
                                            bg: useColorModeValue('gray.200', 'gray.700'),
                                        }} onClick={() => logout()}>Log out</Link>
                                </>
                            )}
                        </Stack>
                    </Box>
                ) : null}
            </Box>
        </>
    )
}

export default Header