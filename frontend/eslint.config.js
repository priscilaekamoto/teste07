import js from '@eslint/js'
import reactHooks from 'eslint-plugin-react-hooks'
import reactRefresh from 'eslint-plugin-react-refresh'
import globals from 'globals'
import tseslint from 'typescript-eslint'

export default tseslint.config(
  { ignores: ['dist'] },
  {
    extends: [js.configs.recommended, ...tseslint.configs.recommended],
    files: ['**/*.{ts,tsx}'],
    languageOptions: {
      ecmaVersion: 2020,
      globals: globals.browser,
    },
    plugins: {
      'react-hooks': reactHooks,
      'react-refresh': reactRefresh,
    },
    rules: {
      ...reactHooks.configs.recommended.rules,
      'react-refresh/only-export-components': [
        'warn',
        { allowConstantExport: true },
      ],
      // Formatting rules
      '@typescript-eslint/indent': ['error', 2],
      'indent': ['error', 2],
      'no-mixed-spaces-and-tabs': 'error',
      'linebreak-style': ['error', 'windows'],
      'quotes': ['error', 'single'],
      'semi': ['error', 'never'],
      'comma-dangle': ['error', 'always-multiline'],
      'no-multiple-empty-lines': ['error', { 'max': 1 }],
      'object-curly-spacing': ['error', 'always'],
      'array-bracket-spacing': ['error', 'never'],
      'max-len': ['error', { 'code': 80 }],
    },
  },
)
