import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Input,
    InputGroup,
    HStack,
    InputRightElement,
    Stack,
    Button,
    Heading,
    Text,
    useColorModeValue,
    Link,
    useToast,
} from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import NextLink from "next/link";
import { useRouter } from 'next/router';
import { useAppStates } from '../lib/AppContext';
import { useFormik } from 'formik';
import { getReasonPhrase } from 'http-status-codes';

const initialValues = {
    email: '',
    password: '',
    confirmPassword: '',
}

export default function SignupCard() {
    const router = useRouter();
    const redirectTo = router.query?.["redirect"]
    const { api, user } = useAppStates();
    const toast = useToast();
    const [loading, setLoading] = useState(false);
    const [showPassword, setShowPassword] = useState(false);

    const formik = useFormik({
        initialValues,
        onSubmit: async (values) => {
            try {
                setLoading(true);
                await api.register(values);
                router.push(redirectTo ? `/login?redirect=${redirectTo}` : `/login`)
                toast({
                    title: 'Success!',
                    status: 'success',
                })
            } catch (e) {
                console.log(e)
                toast({
                    title: 'There is an error occured!',
                    description: `${getReasonPhrase(e.response.status)}`,
                    status: 'error',
                })
            } finally {
                setLoading(false);
            }
        },
    })

    useEffect(() => {
        if (!!user) {
            router.push(redirectTo ? `${decodeURIComponent(redirectTo)}` : `/`);
        }
    }, [user]);

    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                <Stack align={'center'}>
                    <Heading fontSize={'4xl'} textAlign={'center'}>
                        Sign up
                    </Heading>
                    <Text fontSize={'lg'} color={'gray.600'}>
                        to enjoy all of our cool features ??
                    </Text>
                </Stack>
                <form onSubmit={formik.handleSubmit}>
                    <Box
                        rounded={'lg'}
                        bg={useColorModeValue('white', 'gray.700')}
                        boxShadow={'lg'}
                        p={8}
                        minW={'md'}
                    >
                        <Stack spacing={4}>
                            <FormControl id="fullName">
                                <FormLabel>Full name</FormLabel>
                                <Input type="text" />
                            </FormControl>
                            <FormControl id="email" isRequired>
                                <FormLabel>Email address</FormLabel>
                                <Input type="email" value={formik.values.email} onChange={formik.handleChange} />
                            </FormControl>
                            <FormControl id="password" isRequired>
                                <FormLabel>Password</FormLabel>
                                <InputGroup>
                                    <Input type={showPassword ? 'text' : 'password'} value={formik.values.password} onChange={formik.handleChange} />
                                    <InputRightElement h={'full'}>
                                        <Button
                                            variant={'ghost'}
                                            onClick={() =>
                                                setShowPassword((showPassword) => !showPassword)
                                            }>
                                            {showPassword ? <ViewIcon /> : <ViewOffIcon />}
                                        </Button>
                                    </InputRightElement>
                                </InputGroup>
                            </FormControl>
                            <FormControl id="confirmPassword" isRequired>
                                <FormLabel>Confirm your password</FormLabel>
                                <InputGroup>
                                    <Input type={showPassword ? 'text' : 'password'} value={formik.values.confirmPassword} onChange={formik.handleChange} />
                                    <InputRightElement h={'full'}>
                                        <Button
                                            variant={'ghost'}
                                            onClick={() =>
                                                setShowPassword((showPassword) => !showPassword)
                                            }>
                                            {showPassword ? <ViewIcon /> : <ViewOffIcon />}
                                        </Button>
                                    </InputRightElement>
                                </InputGroup>
                            </FormControl>
                            <Stack spacing={10}>
                                <Link as={NextLink} color={'blue.400'} href={redirectTo ? `/login?redirect=${redirectTo}` : `/login`}>Already have an account?</Link>
                                <Button
                                    loadingText="Submitting"
                                    type="submit"
                                    isLoading={loading}
                                    size="lg"
                                    bg={'blue.400'}
                                    color={'white'}
                                    _hover={{
                                        bg: 'blue.500',
                                    }}>
                                    Sign up
                                </Button>
                            </Stack>
                        </Stack>
                    </Box>
                </form>
            </Stack>
        </Flex>
    );
}